# imports
import os
import uuid
import utils
import werkzeug
from flask import Flask
from analyzer import voice_analyzer
from flask_restful import Resource, Api, reqparse


# Instance of application
class SoundCount(Resource):

    """
    An instance of the SoundCount app
    POST: receive a WAV file to process.
    file: the file field.
    """

    def __init__(self):
        pass

    def post(self):

        # Create a temp file, assume bad result.
        payload = {'status': 'failure',
                   'count': 0,
                   'meta': {}}

        # Parse the request into a dict() which contains the raw wav data.
        parse = reqparse.RequestParser()
        parse.add_argument('file', type=werkzeug.datastructures.FileStorage, location='files')
        args = parse.parse_args()

        # Save the data as a temporary file
        filename = str(uuid.uuid4())
        audio_file = args['file']
        audio_file.save(filename)

        # Extract the words and perform an analysis.
        words = utils.speech_rec(filename)
        analysis = voice_analyzer(filename)

        # Tag the words
        payload['meta']['text'] = utils.pos_tagger([words['meta']['text']])
        payload['meta']['gender'] = analysis['gender']
        payload['meta']['age'] = analysis['age']
        payload['meta']['dialect'] = analysis['dialect']

        # Count the words
        for sentence in payload['meta']['text']:
            payload['count'] += len(sentence)

        duration = utils.duration(filename)

        # Check for errors
        if 'error' not in payload:
            payload['status'] = 'success'
            payload['meta']['duration'] = duration

        # Remove temp file
        os.remove(filename)
        return payload


# Create the app and resource (root)
app = Flask(__name__)
api = Api(app)

api.add_resource(SoundCount, '/')
if __name__ == '__main__':
    app.run(debug=True)