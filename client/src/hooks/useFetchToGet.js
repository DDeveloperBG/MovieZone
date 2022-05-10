import { useState, useEffect } from "react";
import { getAuthorizeRequestInitObject } from "../utils/requestHelper";

function useFetchToGet(url, hasToAuthorize) {
  const [state, setState] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    (async () => {
      let initObj = {};
      if (hasToAuthorize) {
        initObj = await getAuthorizeRequestInitObject();
      }
      initObj.method = "GET";

      const response = await fetch(url, initObj);
      const jsonResponse = await response.json();
      setState(jsonResponse);
      setIsLoading(false);
    })();
  }, [url]);

  return [state, isLoading];
}

export default useFetchToGet;
