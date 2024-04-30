import React from 'react';
import styles from './Questions.module.css';
import CardFaq from './CardFaq';

const Questions = () => {
  return (
    <section className={`${styles.main} container margin-top`}>
      <h1 className={styles.mainText}>Como funciona?</h1>
      <CardFaq
        tittle={'Analise os Preços'}
        text={
          'Descubra quanto cada serviço custa e se encaixa no seu bolso. Lembre-se de considerar o número de telas e a qualidade de transmissão.'
        }
        type={'cifrao'}
      />
      <CardFaq
        tittle={'Decida com Confiança'}
        text={
          'Escolha o streaming que atenda às suas necessidades e comece a aproveitar o melhor do entretenimento.'
        }
        type={'lupa'}
      />
      <CardFaq
        tittle={'Explore as Opções'}
        text={
          'Navegue pelos serviços de streaming e compare suas características. Veja quais oferecem os filmes e séries que você ama.'
        }
        type={'cifrao'}
      />
    </section>
  );
};

export default Questions;
