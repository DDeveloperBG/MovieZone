import React, { useState, useEffect, useCallback } from "react";
import PropTypes from "prop-types";
import DurationPickerColumn from "./DurationPickerColumn/DurationPickerColumn";

import "./DurationPicker.scss";

DurationPicker.propTypes = {
  onChange: PropTypes.func,
  initialDuration: PropTypes.shape({
    hours: PropTypes.number,
    minutes: PropTypes.number,
  }),
  maxHours: PropTypes.number,
  noHours: PropTypes.bool,
};

DurationPicker.defaultProps = {
  maxHours: 12,
  onChange: () => {},
  initialDuration: { hours: 0, minutes: 0 },
  noHours: false,
};

function DurationPicker(props) {
  const { onChange, maxHours, initialDuration, noHours } = props;
  const [isSmallScreen, setIsSmallScreen] = useState(undefined);
  let duration = { ...initialDuration };

  const onChangeHours = useCallback((hours) => {
    duration.hours = hours;
    onChange(duration);
  }, []);
  const onChangeMinutes = useCallback((minutes) => {
    duration.minutes = minutes;
    onChange(duration);
  }, []);

  useEffect(() => {
    const resizeHandler = () => {
      if (window.innerWidth <= 400) {
        setIsSmallScreen(true);
      } else {
        setIsSmallScreen(false);
      }
    };
    resizeHandler();
    window.addEventListener("resize", resizeHandler);
    return () => {
      window.removeEventListener("resize", resizeHandler);
    };
  }, []);

  // // execute callback prop
  // useEffect(() => {
  //   onChange(duration);
  // }, [duration, onChange]);
  return (
    <div className="duration-picker-wrapper">
      <div className="rdp-picker">
        {!noHours && (
          <DurationPickerColumn
            unit="hours"
            maxHours={maxHours}
            isSmallScreen={isSmallScreen}
            initial={initialDuration.hours}
            onChange={onChangeHours}
          />
        )}
        <DurationPickerColumn
          unit="mins"
          isSmallScreen={isSmallScreen}
          initial={initialDuration.minutes}
          onChange={onChangeMinutes}
        />
      </div>
    </div>
  );
}

export default DurationPicker;
