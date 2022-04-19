import firebase from "firebase/compat/app";
import "firebase/compat/auth";

const firebaseConfig = {
  apiKey: "AIzaSyC8VXD-mzzy_FBuMQQPEe-gxNHF8R9eC24",
  authDomain: "moviezone-e34cc.firebaseapp.com",
  projectId: "moviezone-e34cc",
  storageBucket: "moviezone-e34cc.appspot.com",
  messagingSenderId: "127425447663",
  appId: "1:127425447663:web:df2ffebdec9cb3580ffd1a",
  measurementId: "G-B0BDFYERJJ",
};

if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
}

export default firebase;
export const auth = firebase.auth();

// Firebase Analytics
// import { getAnalytics } from "firebase/analytics";
// const app = firebase.initializeApp(firebaseConfig);
// const analytics = getAnalytics(app);
