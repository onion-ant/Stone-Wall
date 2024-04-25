import Carousel from '../Carousel/Carousel';
import Header from '../Header/Header';
import styles from './Home.module.css';

const Home = () => {
  return (
    <div className={styles.background}>
      <Header />
      <Carousel />
    </div>
  );
};

export default Home;
