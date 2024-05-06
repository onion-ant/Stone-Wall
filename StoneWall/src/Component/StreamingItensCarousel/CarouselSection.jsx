import React from 'react';
import styles from './CarouselSection.module.css';

const Movies = () => {
  const [images] = React.useState([
    '../../Assets/TestAssets/2uY8aYmc86UL4N86D2spkWzYKOd.jpg',
    '../../Assets/TestAssets/e8pI4XkYgUMuSJ8cEFbJE18wc4e.jpg',
    '../../Assets/TestAssets/fZJSBHJKNR7Xz3CiEsAawd7bbDh.jpg',
  ]);
  const [autoPlay, setAutoPlay] = React.useState(true);
  const [current, setCurrent] = React.useState([1, 2, 0]);
  let timeOut = null;
  React.useEffect(() => {
    timeOut =
      autoPlay &&
      setTimeout(() => {
        setCurrent((prevIds) => {
          return [prevIds[1], prevIds[2], prevIds[0]];
        });
      }, 2000);
  });
  return (
    <div className={styles.background}>
      <div
        className={`${styles.movies} container'`}
        onMouseEnter={() => {
          setAutoPlay(false);
          clearTimeout(timeOut);
        }}
        onMouseLeave={() => {
          setAutoPlay(true);
          clearTimeout(timeOut);
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
  );
};

export default Movies;
