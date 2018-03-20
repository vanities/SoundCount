import wave
import nltk
import logging
import contextlib
import speech_recognition as sr

from os import path
from recognizers import sphinx

def duration(filename):
    with contextlib.closing(wave.open(filename, 'r')) as f:
        frames = f.getnframes()
        rate = f.getframerate()
        return frames / float(rate)


def speech_rec(filename):
    audio_file = path.join(path.dirname(path.realpath(__file__)), filename)
    r = sr.Recognizer()

    try:
        with sr.AudioFile(audio_file) as source:
            audio = r.record(source)
    except ValueError:
        print("boom!")

    return sphinx(r, audio)


def word_count(phrase):
    return len(phrase.split())


def pos_tagger(words):
    tag_words = []

    for word in words:
        tag_words.append(nltk.pos_tag(word))

    return tag_words


# Attempt to set up a Python3 logger than will print custom messages
# based on each message's logging level.
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


class Log():

    def __init__(self):
        self.logger = logging.getLogger('sound_count_log')
        self.logger.setLevel(logging.DEBUG)

        self.formatter = CustomFormatter()

        self.handler = logging.StreamHandler()
        self.handler.setFormatter(self.formatter)

        self.logger.addHandler(self.handler)

    def debug(self, message):
        self.logger.debug("DEBUG: {0}".format(message))

    def info(self, message):
        self.logger.info("INFO: {0}".format(message))

    def warning(self, message):
        self.logger.info("WARNING: {0}".format(message))

    def error(self, message):
        self.logger.info("ERROR: {0}".format(message))

    def critical(self, message):
        self.logger.info("CRITICAL: {0}".format(message))

logger = Log()

# logger.debug("This message")
# logger.info("That message")
# logger.warning("Bad message")
# logger.error("uh oh message")
# logger.critical("FFUUUUUU!")
