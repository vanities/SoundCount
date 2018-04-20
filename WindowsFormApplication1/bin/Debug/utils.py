import os
import wave
import nltk
import logging
import contextlib
import environment as env
import speech_recognition as sr

from os import path
from recognizers import sphinx

# Duration, opens a wave file and finds the number of frames per rate
# Input:  a filename with a .wav file
# Output: returns the framerate
def duration(filename):
    # open the wave file
    with contextlib.closing(wave.open(filename, 'r')) as f:
        frames = f.getnframes()         # get frames
        rate = f.getframerate()         # get rate
        return frames / float(rate)     # return framerate


# Speech recognizer, recognizes speech from a wav file and returns the text
# Speech -> Text
# Input:  a file name of a .wav file
# Output: The recognized speech as text
def speech_rec(filename):

    audio_file = path.join(path.dirname(path.realpath(__file__)), filename)
	
    # instantiate a speech recognizer object
    r = sr.Recognizer()
    # try to open the audio file
    try:
        with sr.AudioFile(audio_file) as source:
            audio =  r.record(source)
        #with open(audio_file) as audio:
    except ValueError:
        return

    # return the speech to text
    return sphinx(r, audio)


# Word Count, counts the word in a sentence
# Input:  a sentence phrase
# Output: the length of the sentence
def word_count(phrase):
    return len(phrase.split())


# Part of Speech tagger, tags speech depending on the words in the sentence
# Input:  a sentence of words
# Output: a list of tagged words
def pos_tagger(words):
    tag_words = []      # empty list of tagged words

    # append all of the words into the tagged words
    for word in words:
        tag_words.append(nltk.pos_tag(word))

    # return the tagged words
    return tag_words


# Attempt to set up a Python3 logger than will print custom messages
# based on each message's logging level.
# Overrides the default logger
# Input:  the logger message, ex. warning, info, error
# Output: returns the result
class CustomFormatter(logging.Formatter):
    """
    Modify the way messages are displayed.
    """
    def __init__(self):
        super().__init__(fmt="%(asctime)s: %(msg)s", datefmt=None, style='%')

    def format(self, record):

        # Remember the original format
        format_orig = self._fmt

        if record.levelno == logging.DEBUG:
            self._fmt = "DEBUG: %(msg)s"

        # Call the original formatter to do the grunt work
        result = logging.Formatter.format(self, record)

        # Restore the original format
        self._fmt = format_orig

        return result

# Class for a Logger, Handles the logging for our application
class Log():

    # initialization
    def __init__(self):
        # gets the logger from the sound count log
        self.logger = logging.getLogger('sound_count_log')
        # set the level of the degub message
        self.logger.setLevel(logging.DEBUG)

        # ceate a custom formatter
        self.formatter = CustomFormatter()

        # stdout logging
        self.stdout_handler = logging.StreamHandler()
        self.stdout_handler.setFormatter(self.formatter)

        # file handling
        self.file_handler = logging.FileHandler(env.app_vars['LOG_PATH'])
        self.file_handler.setFormatter(self.formatter)

        # add for handling for files and stdout
        self.logger.addHandler(self.stdout_handler)
        self.logger.addHandler(self.file_handler)

    # custom debugger message
    def debug(self, message):
        self.check_size()
        self.logger.debug("DEBUG: {0}".format(message))

    # custom info message
    def info(self, message):
        self.check_size()
        self.logger.info("INFO: {0}".format(message))

    # custom warning message
    def warning(self, message):
        self.check_size()
        self.logger.info("WARNING: {0}".format(message))

    # custom error message
    def error(self, message):
        self.check_size()
        self.logger.info("ERROR: {0}".format(message))

    # custom criticals message
    def critical(self, message):
        self.check_size()
        self.logger.info("CRITICAL: {0}".format(message))

    # checks the see if the size is too big, removes the log file if 512 Mb
    def check_size(self):
        if os.path.getsize(env.app_vars['LOG_PATH']) > env.app_vars['LOG_MAXSIZE']:
            os.remove(env.app_vars['LOG_PATH'])

# instantiate a logger
logger = Log()

# TESTING THE LOGGER
# logger.debug("This message")
# logger.info("That message")
# logger.warning("Bad message")
# logger.error("uh oh message")
# logger.critical("FFUUUUUU!")
