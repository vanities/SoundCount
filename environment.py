import os
from sklearn.externals import joblib

creds = {
    'BING_KEY': '',
    'GOOGLE_CLOUD_SPEECH': '',

    'HOUNDIFY_CLIENT_ID': '',
    'HOUNDIFY_CLIENT_KEY': '',

    'WIT_AI_KEY': '',

    'IBM_USERNAME': '',
    'IBM_PASSWORD': ''}

# grab the pickel'd libraries for the recognizer
clffg = joblib.load(os.path.join('models', 'cfl_gender.pkl'))
clffa = joblib.load(os.path.join('models', 'cfl_age.pkl'))
clffd = joblib.load(os.path.join('models', 'cfl_dialect.pkl'))
