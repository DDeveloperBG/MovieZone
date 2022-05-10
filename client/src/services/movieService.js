import { getIdToken } from "../utils/firebase";

export async function getMovieFileUrl(movieId) {
  const idToken = await getIdToken();
  return `${process.env.REACT_APP_FILE_API_GET_MOVIE_FILE_URL}?movieId=${movieId}&userIdToken=${idToken}`;
}

export function addMovie(movieFormData) {
  getIdToken().then((tokenId) => {
    fetch(process.env.REACT_APP_MOVIE_API_ADD_MOVIE_URL, {
      method: "POST",
      headers: {
        authorization: `Bearer ${tokenId}`,
      },
      body: movieFormData,
    });
  });
}
