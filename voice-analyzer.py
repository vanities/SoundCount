import numpy as np
import librosa
from sklearn.externals import joblib

# grab the pickel'd libraries for the recognizer
clffg = joblib.load('models/cfl_gender.pkl')
clffa = joblib.load('models/cfl_age.pkl') 
clffd = joblib.load('models/cfl_dialect.pkl')

# use the file created
y, sr = librosa.load("robertwalters.wav", sr=22050)

# finds the spectrogram from the voice
spectrogram = np.abs(librosa.stft(y))

melspec = librosa.feature.melspectrogram(y=y, sr=sr)
stft = np.abs(librosa.stft(y))
mfccs = np.mean(librosa.feature.mfcc(y=y, sr=sr, n_mfcc=40).T, axis=0)
mel = np.mean(librosa.feature.melspectrogram(y, sr=sr).T, axis=0)
contrast = np.mean(librosa.feature.spectral_contrast(S=stft, sr=sr).T, axis=0)
tonnetz = np.mean(librosa.feature.tonnetz(y=librosa.effects.harmonic(y), sr=sr).T, axis=0)
chroma = np.mean(librosa.feature.chroma_stft(S=stft, sr=sr).T,axis=0)

# create the features from the above
features = np.hstack([mfccs,chroma,mel,contrast,tonnetz])

# rehapse the features matrix
features = features.reshape(1, -1)

# make a prediction from the features
pred = clffg.predict(features)

# print the predictions
print("Gender Prediction: ", pred)
pred = clffa.predict(features)
print("Age Prediction: ", pred)
pred = clffd.predict(features)
print("Dialect Prediction: ", pred)
