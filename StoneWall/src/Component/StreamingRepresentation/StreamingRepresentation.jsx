import { useEffect, useState } from 'react';
import styles from './StreamingRepresentation.module.css';
import SideMenu from '../SideMenu/SideMenu';
import ItemsCatalogo from './ItemsCatalogo/ItemsCatalogo';
import useFetch from '../useFetch';
import SearchAnalysis from './AnalysisForm/SearchAnalysis';
import Select from './Select/Select';

const StreamingRepresentation = () => {
  const [selectedStreaming, setSelectedStreaming] = useState('');
  const [streamingType, setStreamingType] = useState('');
  const [genres, setGenres] = useState('');
  const [data, setData] = useState('');
  const [search, setSearch] = useState('');
  const [selectValue, setSelectValue] = useState('');
  useFetch('https://localhost:7282/Streamings', setData);
  const signatureOptions = [
    { name: 'Assinatura', id: 'subscription' },
    { name: 'AddOn', id: 'addon' },
    { name: 'Comprar', id: 'buy' },
    { name: 'Alugar', id: 'rent' },
  ];
  const genreOptions = [
    { name: 'Ação', id: 'action' },
    { name: 'Aventura', id: 'adventure' },
    { name: 'Comédia', id: 'comedy' },
    { name: 'Policial', id: 'crime' },
    { name: 'Documentário', id: 'documentary' },
    { name: 'Drama', id: 'drama' },
    { name: 'Família', id: 'family' },
    { name: 'Fantasia', id: 'fantasy' },
    { name: 'História', id: 'history' },
    { name: 'Terror', id: 'horror' },
    { name: 'Música', id: 'music' },
    { name: 'Mistério', id: 'mystery' },
    { name: 'Notícia', id: 'news' },
    { name: 'Reality', id: 'reality' },
    { name: 'Romance', id: 'romance' },
    { name: 'Ficção Científica', id: 'scifi' },
    { name: 'Talk Show', id: 'talk' },
    { name: 'Suspense', id: 'thriller' },
    { name: 'Guerra', id: 'war' },
    { name: 'Velho Oeste', id: 'western' },
  ];
  let url;
  if (
    (search && !selectedStreaming && !streamingType) ||
    (genres && !selectedStreaming && !streamingType)
  ) {
    url = `https://localhost:7282/Items?offset=60&sizeParams=w300_and_h450_bestv2&language=pt-BR${
      search ? '&name=' + search : ''
    }${genres ? '&genreId=' + genres : ''}`;
  } else {
    url =
      selectedStreaming || search || genres
        ? `https://localhost:7282/Streamings/${selectedStreaming}${
            streamingType ? '?streamingType=' + streamingType + '&' : '?'
          }sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=60${
            search ? '&name=' + search : ''
          }${genres ? '&genreId=' + genres : ''}`
        : 'https://localhost:7282/Items?offset=60&sizeParams=w300_and_h450_bestv2&language=pt-BR';
  }
  url;
  useEffect(() => {
    if (!selectValue) {
      setStreamingType('');
    }
  }, [selectValue]);
  console.log(url);
  return (
    <div className={styles.background}>
      <SideMenu />
      <div className={`${styles.catalogo}`}>
        {data && (
          <div className={styles.selects}>
            {selectValue && (
              <Select
                setItem={setStreamingType}
                jsonOptions={signatureOptions}
                texto={'Selecione a forma'}
              />
            )}
            <Select
              setItem={setSelectedStreaming}
              jsonOptions={data}
              texto={'Selecione um Streaming'}
              streamingIsSelected={setSelectValue}
            />
            <Select
              setItem={setGenres}
              jsonOptions={genreOptions}
              texto={'Selecione um Gênero'}
            />
            <SearchAnalysis setSearch={setSearch} />
          </div>
        )}
        <ItemsCatalogo urlFetch={url} />
      </div>
    </div>
  );
};

export default StreamingRepresentation;
