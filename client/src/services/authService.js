import { auth } from "../utils/firebase";

export const registerUser = (username) => {
  const url = process.env.REACT_APP_AUTH_API_URL;
  auth.currentUser.getIdToken(true).then((idToken) => {
    fetch(url, {
      method: "POST",
      headers: {
        "content-type": "application/json",
      },
      body: JSON.stringify({
        idToken: idToken,
        username: username,
      }),
    }).catch((e) => console.error(e));
  });
};
