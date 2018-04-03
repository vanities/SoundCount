import utils
import speech_recognition as sr

from environment import creds

# Sphinx Recognizer, Free to use but only decent at recognizing
def sphinx(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_sphinx(audio)
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Sphinx could not understand audio"
        logger.error("Sphinx couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Sphinx error; {0}".format(e)
        logger.error("Sphinx error; {0}".format(e))

    payload.update({'meta': meta})
    return payload

# Googles recognizer, not free to use but spot on with recognizing
def google(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_google(audio)
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Google Speech Recognizer could not understand audio"
        logger.error("Google Speech Recognizer couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from Google Speech Recognition service; {0}".format(e)
        logger.error("Could not request results from Google Speech Recognition service; {0}".format(e))

    payload.update({'meta': meta})
    return payload


def google_sound_cloud(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_google_cloud(audio, credentials_json=creds['GOOGLE_CLOUD_SPEECH'])
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Google Cloud Speech could not understand audio"
        logger.error("Google Cloud Speech couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from Google Cloud Speech service; {0}".format(e)
        logger.error("Could not request results from Google Cloud speech Recognition service; {0}".format(e))

    payload.update({'meta': meta})
    return payload

# Wit recognizer, not free decent at recognizing
def wit(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_wit(audio, key=creds['WIT_AI_KEY'])
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Wit.ai could not understand audio"
        logger.error("Wit.ai couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from Wit.ai service; {0}".format(e)
        logger.error("Could not request results from Wit.ai service; {0}".format(e))

    return payload

# Bing recognizer, not free but really good at recongizing
def bing(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_bing(audio, key=creds['BING_KEY'])
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Microsoft Bing Voice Recognition could not understand audio"
        logger.error("Microsoft Bing Voice Recognition  couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from Microsoft Bing Voice Recognition service; {0}".format(e)
        logger.error("Could not request results from Microsoft Bing Voice Recognition service; {0}".format(e))

    return payload

# Houndify recognizer, not free and decent at recognizing
def houndify(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_houndify(audio, client_id=creds['HOUNDIFY_CLIENT_ID'], client_key=creds['HOUNDIFY_CLIENT_KEY'])
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "Houndify could not understand audio"
        logger.error("Houndify couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from Houndify service; {0}".format(e)
        logger.error("Could not request results from Houndify service; {0}".format(e))

    payload.update({'meta': meta})
    return payload

# IBM's recognizer, not free pretty good and accurate recognizer
def ibm(r, audio):

    payload = {'count': 'invalid'}
    meta = {}

    try:
        phrase = r.recognize_ibm(audio, username=creds['IBM_USERNAME'], password=creds['IBM_PASSWORD'])
        payload['count'] = utils.word_count(phrase)
        meta['text'] = phrase.split()
    except sr.UnknownValueError:
        payload['error'] = "IBM Speech to Text could not understand audio"
        logger.error("IBM Speech to Text couldn't understand audio")
    except sr.RequestError as e:
        payload['error'] = "Could not request results from IBM Speech to Text service; {0}".format(e)
        logger.error("Could not request results from IBM Speech to Text service; {0}".format(e))

    payload.update({'meta': meta})
    return payload
