import { useState, useEffect } from "react";
import Video from "twilio-video";

import Room from "./Room/Room";
import Spinner from "../Shared/Spinner/Spinner";

import useFetchToGet from "../../hooks/useFetchToGet";

const VideoChat = () => {
  const [room, setRoom] = useState(null);
  const [isLoadingRoomData, setIsLoadingRoomData] = useState(true);
  const [callData, isLoadingCallData] = useFetchToGet(
    `${process.env.REACT_APP_VIDEOCHAT_API_CREATE_CALL_URL}?calledUserId=${2}`,
    true
  );
  const { roomId, accessToken } = callData ?? {
    roomId: null,
    accessToken: null,
  };

  useEffect(() => {
    if (!accessToken) {
      return;
    }

    Video.connect(accessToken, {
      name: roomId,
    })
      .then((room) => {
        setIsLoadingRoomData(false);
        setRoom(room);
      })
      .catch((err) => {
        console.error(err);
        setIsLoadingRoomData(false);
      });
  }, [roomId, accessToken]);

  if (isLoadingCallData || isLoadingRoomData) {
    return <Spinner />;
  }

  return <Room roomName={roomId} room={room} />;
};

export default VideoChat;
