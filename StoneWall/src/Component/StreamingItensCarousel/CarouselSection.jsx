import React from 'react';
import PropTypes from 'prop-types';
import styles from './CarouselSection.module.css';

const CarouselSection = () => {
  const [autoPlay, setAutoPlay] = React.useState(true);
  const [error, setError] = React.useState();
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
    fetch('https://localhost:7282/Items?offset=3')
      .then((x) => x.json())
      .then((xs) => {
        setImages(
          xs.items.map((x) => {
            return x.posterPath;
          }),
        );
        setError(false);
      })
      .catch(() => {
        setError(true); // <== this **WILL** be invoked on exception
      });
    incrementCounter();
  }, []);
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
            onMouseLeave={() => {
              incrementCounter();
              setAutoPlay(true);
            }}
          >
            {images.map((image, index) => (
              <img
                src={image}
                key={index}
                alt=""
                className={
                  autoPlay == false && current[index] == 1
                    ? `carousel_item_` + current[index] + ' carousel_paused'
                    : `carousel_item_` + current[index]
                }
              />
            ))}
          </div>
        </div>
      )}
    </>
  );
};

export default CarouselSection;
