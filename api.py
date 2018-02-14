
import os
import uuid
import utils
import werkzeug
from flask import Flask
from flask_restful import Resource, Api, reqparse


class SoundCount(Resource):
    def __init__(self):
        pass

    def post(self):

        filename = str(uuid.uuid4())
        payload = {'status': 'failure'}

        parse = reqparse.RequestParser()
        parse.add_argument('file', type=werkzeug.datastructures.FileStorage, location='files')
        args = parse.parse_args()

        audioFile = args['file']
        audioFile.save(filename)

        payload = utils.speech_rec(filename)
        d = utils.duration(filename)

        if 'error' not in payload:
            payload['status'] = 'success'

        payload['meta']['duration'] = d

        os.remove(filename)
        return payload


app = Flask(__name__)
api = Api(app)

api.add_resource(SoundCount, '/')
if __name__ == '__main__':
    app.run(debug=True)
