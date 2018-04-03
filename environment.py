import os
from sklearn.externals import joblib

# change this to make a bigger log
LOG_SIZE = 512

# dictionary of application variables
# used for the log file and size
app_vars = {
    'LOG_PATH': 'logs/log.log',
    'LOG_MAXSIZE': LOG_SIZE * 1024}

# set the API keys if wanting to use non-free speech recognizers
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
