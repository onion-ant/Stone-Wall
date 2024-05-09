import React from 'react';
import styles from './CarouselSection.module.css';

const CarouselSection = () => {
  const [autoPlay, setAutoPlay] = React.useState(true);
  const [error, setError] = React.useState(true);
  const [streamings, setStreamings] = React.useState();
  const [current, setCurrent] = React.useState([1, 2, 0]);
  const [images, setImages] = React.useState([]);
  const intervalRef = React.useRef(null);

  const incrementCounter = () => {
    intervalRef.current = setInterval(() => {
      setCurrent((prevIds) => {
        return [prevIds[1], prevIds[2], prevIds[0]];
      });
    }, 2000);
  };
  const resetCounter = () => {
    clearInterval(intervalRef.current);
    intervalRef.current = null;
  };
  React.useEffect(() => {
    fetch(
      'https://localhost:7282/Items?atLeast=3&sizeParams=w600_and_h900_bestv2&pageNumber=1&offset=3',
    )
      .then((x) => x.json())
      .then((xs) => {
        console.log(xs);
        setStreamings(
          xs.map((x) => {
            return x.streamings;
          }),
        );
        setImages(
          xs.map((x) => {
            return x.posterPath;
          }),
        );
        setError(false);
      })
      .catch(() => {
        setError(true);
      });
    incrementCounter();
  }, []);
  // console.log(streamings);
  return (
    <>
      {!error && (
        <div className={styles.background}>
          <div
            className={`${styles.movies} container'`}
            onMouseEnter={() => {
              resetCounter();
              setAutoPlay(false);
            }}
            onTouchStart={() => {
              if (autoPlay) {
                setAutoPlay(false);
                resetCounter();
              } else {
                incrementCounter();
                setAutoPlay(true);
              }
            }}
            onMouseLeave={() => {
              incrementCounter();
              setAutoPlay(true);
            }}
          >
            {images.map((image, index) => (
              <div
                key={index}
                className={
                  autoPlay == false && current[index] == 1
                    ? `cardItems cardItems_N_` +
                      current[index] +
                      ' carousel_paused'
                    : `cardItems cardItems_N_` + current[index]
                }
                style={{ backgroundImage: `url(${image})` }}
              >
                <div className={'streamingsItem'}>
                  {streamings[0].map((x, indexStreaming) => (
                    <div key={indexStreaming} className={styles.streaming}>
                      <img
                        src={`../../Assets/${x.streamingId}Square.svg`}
                        alt=""
                      />
                      <p>{x.type}</p>
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
    </>
  );
};

export default CarouselSection;
