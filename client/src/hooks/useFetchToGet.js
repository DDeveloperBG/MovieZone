import { useState, useEffect } from "react";

function useFetchToGet(url, initialValue) {
  const [state, setState] = useState(initialValue);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    fetch(url)
      .then((res) => res.json())
      .then((result) => {
        setState(result);
        setIsLoading(false);
      });
  }, [url]);

  return [state, isLoading];
}

export default useFetchToGet;
