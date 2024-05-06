import React from 'react';
import styles from './CarouselSection.module.css';

const Movies = () => {
  const [images, setImages] = React.useState([]);
  const [stopV, setStopV] = React.useState(false);
  function handleClick() {
    setStopV(!stopV);
  }
  React.useEffect(() => {
    setImages([
      '../../Assets/TestAssets/2uY8aYmc86UL4N86D2spkWzYKOd.jpg',
      '../../Assets/TestAssets/e8pI4XkYgUMuSJ8cEFbJE18wc4e.jpg',
      '../../Assets/TestAssets/fZJSBHJKNR7Xz3CiEsAawd7bbDh.jpg',
    ]);
  }, []);
  return (
    <div className={styles.background}>
      <div className={`${styles.movies} container'`} onClick={handleClick}>

      </div>
    </div>
  );
};

export default Movies;
