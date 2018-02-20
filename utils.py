import wave
import nltk
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

    with sr.AudioFile(audio_file) as source:
        audio = r.record(source)

    return sphinx(r, audio)


def word_count(phrase):
    return len(phrase.split())

def pos_tagger(words):

    #words = [['hello', 'there', '.']]
    tag_words = []

    for word in words:
        tag_words.append(nltk.pos_tag(word))

    return tag_words
