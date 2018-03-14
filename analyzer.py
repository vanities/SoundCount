import numpy as np
import librosa
import environment as env


def voice_analyzer(filename):
    # use the file created
    y, sr = librosa.load(filename, sr=22050)

    # finds the spectrogram from the voice
    # spectrogram = np.abs(librosa.stft(y))
    # melspec = librosa.feature.melspectrogram(y=y, sr=sr)

    stft = np.abs(librosa.stft(y))
    mfccs = np.mean(librosa.feature.mfcc(y=y, sr=sr, n_mfcc=40).T, axis=0)
    mel = np.mean(librosa.feature.melspectrogram(y, sr=sr).T, axis=0)
    contrast = np.mean(librosa.feature.spectral_contrast(S=stft, sr=sr).T, axis=0)
    tonnetz = np.mean(librosa.feature.tonnetz(y=librosa.effects.harmonic(y), sr=sr).T, axis=0)
    chroma = np.mean(librosa.feature.chroma_stft(S=stft, sr=sr).T,axis=0)

    # create the features from the above
    features = np.hstack([mfccs,chroma,mel,contrast,tonnetz])

    # reshape the features matrix
    features = features.reshape(1, -1)

    # make a prediction from the features
    # print the predictions
    info = dict()
    info['gender'] = env.clffg.predict(features)[0]#pred = env.clffg.predict(features)
    info['age'] = env.clffa.predict(features)[0] #pred = env.clffa.predict(features)
    info['dialect'] = env.clffd.predict(features)[0] # pred = env.clffd.predict(features)
    return info
