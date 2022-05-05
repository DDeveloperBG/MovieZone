import { auth } from "../utils/firebase";

export async function getMovieFileUrl(movieId) {
  const idToken = await auth.currentUser.getIdToken(true);
  return `${process.env.REACT_APP_FILE_API_GET_MOVIE_FILE_URL}?movieId=${movieId}&userIdToken=${idToken}`;
}
