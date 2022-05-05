import { useState, useEffect } from "react";
import { auth } from "../utils/firebase";

function useFetchToGet(url, hasToAuthorize) {
  const [state, setState] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    (async () => {
      const body = { method: "GET", headers: {} };
      if (hasToAuthorize) {
        body.headers.authorization = `Bearer ${await auth.currentUser.getIdToken(
          true
        )}`;
      }

      const response = await fetch(url, body);
      const jsonResponse = await response.json();
      setState(jsonResponse);
      setIsLoading(false);
    })();
  }, [url]);

  return [state, isLoading];
}

export default useFetchToGet;
