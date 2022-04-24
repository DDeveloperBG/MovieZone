import { auth } from "../utils/firebase";

export const checkIsUsernameUsed = async (username) => {
  const url = process.env.REACT_APP_AUTH_API_URL;
  const response = await fetch(`${url}?username=${username}`);
  const isUsernameUsed = (await response.json()).isUsernameUsed;

  return isUsernameUsed;
};

export const registerUser = async (username) => {
  const url = process.env.REACT_APP_AUTH_API_URL;
  const idToken = await auth.currentUser.getIdToken(true);
  await fetch(url, {
    method: "POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      idToken: idToken,
      username: username,
    }),
  });
};
