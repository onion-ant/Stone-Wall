import { useEffect, useState } from 'react';
import styles from './SingleStreaming.module.css';
import useFetch from '../../useFetch';

const SingleStreaming = () => {
  const [data, setData] = useState('');
  const [isClicked, setIsClicked] = useState(false);
  const [direction, setDirection] = useState('');
  const [ids, setIds] = useState([1, 2, 3, 4, 5, 6, 7, 8, 9, 0]);
  const [randomNumber, setRandomNumber] = useState('');
  useFetch('https://localhost:7282/Streamings', setData);
  function handleClick() {
    setIsClicked(true);
  }
  function handleScroll(event) {
    if (event.deltaY > 0) {
      setDirection('DOWN');
      setIds((prevIds) => {
        return [
          prevIds[9],
          prevIds[0],
          prevIds[1],
          prevIds[2],
          prevIds[3],
          prevIds[4],
          prevIds[5],
          prevIds[6],
          prevIds[7],
          prevIds[8],
        ];
      });
    } else {
      setDirection('UP');
      setIds((prevIds) => {
        return [
          prevIds[1],
          prevIds[2],
          prevIds[3],
          prevIds[4],
          prevIds[5],
          prevIds[6],
          prevIds[7],
          prevIds[8],
          prevIds[9],
          prevIds[0],
        ];
      });
    }
  }
  useEffect(() => {
    setRandomNumber(Math.floor(Math.random() * data.length));
  }, [data]);
  return (
    <>
      {data && (
        <div
          className={styles.streaming}
          style={{
            position: 'relative',

          }}
          onWheel={handleScroll}
          onTouchMove={handleScroll}
        >
          {ids &&
            data.map((item, index) => {
              return (
                <img
                  key={index}
                  id={isClicked ? ids[index] + direction : ''}
                  src={`../../../Assets/${data[index].id}Square.svg`}
                  alt=""
                  className={styles.imageStreaming}
                  onClick={handleClick}
                  style={{
                    position: 'absolute',
                  }}
                />
              );
            })}

          <p></p>
        </div>
      )}
    </>
  );
};

export default SingleStreaming;
