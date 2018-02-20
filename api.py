# imports
import os
import uuid
import utils
import nltk
import werkzeug
from flask import Flask
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

        # create a temp file, assume bad result.
        filename = str(uuid.uuid4())
        payload = {'status': 'failure',
                   'text': '',
                   'meta': {}}

        # parse the request into a dict() which conatins the raw wav data.
        parse = reqparse.RequestParser()
        parse.add_argument('file', type=werkzeug.datastructures.FileStorage, location='files')
        args = parse.parse_args()

        # Sove the data
        audioFile = args['file']
        audioFile.save(filename)

        # count the number of words spoken, update playload in progress.
        words = utils.speech_rec(filename)

        print([words])

        payload['text'] = utils.pos_tagger([words])
        d = utils.duration(filename)

        # Check for errors
        if 'error' not in payload:
            payload['status'] = 'success'
            payload['meta']['duration'] = d

        os.remove(filename)
        return payload


# Create the app and resource (root)
app = Flask(__name__)
api = Api(app)

api.add_resource(SoundCount, '/')
if __name__ == '__main__':
    app.run(debug=True)
