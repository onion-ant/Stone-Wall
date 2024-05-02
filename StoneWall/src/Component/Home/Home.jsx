import CarouselMenu from '../Carousel/CarouselMenu';
import Header from '../Header/Header';
import Movies from '../Movies/Movies';
import Questions from '../Questions/Questions';
import styles from './Home.module.css';

const Home = () => {
  return (
    <>
      <div className={styles.background}>
        <Header />
        <CarouselMenu />
      </div>
      <Questions />
      <div className={styles.backgroundPurple}>
        <Movies />
      </div>
    </>
  );
};

export default Home;
