import { getIdToken } from "../utils/firebase";

export async function getAuthorizeRequestInitObject() {
  const body = { headers: {} };
  const idToken = await getIdToken();
  body.headers.authorization = `Bearer ${idToken}`;

  return body;
}
