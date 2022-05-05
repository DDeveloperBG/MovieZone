import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { Player, Video, DefaultUi, DefaultControls } from "@vime/react";

import Spinner from "../Shared/Spinner/Spinner";

import { getMovieFileUrl } from "../../services/movieService";

import "@vime/core/themes/default.css";

function MoviePlayer() {
  const [movieFileUrl, setMovieFileUrl] = useState("");
  const [searchParams] = useSearchParams();
  const id = searchParams.get("id");
  useEffect(() => {
    (async () => {
      const movieUrl = await getMovieFileUrl(id);
      setMovieFileUrl(movieUrl);
    })();
  }, [id]);

  // const [currentTime, setCurrentTime] = useState(0);

  // const onTimeUpdate = (event) => {
  //   setCurrentTime(event.detail);
  // };

  if (movieFileUrl === "") {
    return (
      <div className="container">
        <Spinner />
      </div>
    );
  }
  // currentTime={currentTime}
  // onVmCurrentTimeChange={onTimeUpdate}
  return (
    <div className="container">
      <Player style={{ "--vm-player-theme": "#f53838" }} loop={false}>
        <Video crossOrigin>
          <source data-src={movieFileUrl} type="video/mp4" />
        </Video>

        <DefaultUi noControls>
          <DefaultControls />
        </DefaultUi>
      </Player>
    </div>
  );
}

export default MoviePlayer;
