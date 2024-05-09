import React from 'react';
import CarouselMenu from '../HomeStreamingCarousel/CarouselMenu';
import Header from '../Header/Header';
import CarouselSection from '../StreamingItensCarousel/CarouselSection';
import Questions from '../Questions/Questions';
import styles from './Home.module.css';
import Footer from '../Footer/Footer';

const Home = () => {
  React.useEffect(() => {}, []);
  return (
    <>
      <div className={styles.background}>
        <Header />
        <CarouselMenu />
      </div>
      <Questions />
      <div className={styles.backgroundPurple}>
        <CarouselSection />
      </div>
      <Footer />
    </>
  );
};

export default Home;
