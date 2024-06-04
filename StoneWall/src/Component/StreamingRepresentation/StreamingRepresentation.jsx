import { useState } from 'react';
import styles from './StreamingRepresentation.module.css';
import SideMenu from './SideMenu/SideMenu';
import ItemsCatalogo from './ItemsCatalogo/ItemsCatalogo';
import useFetch from '../useFetch';
import SearchAnalysis from './AnalysisForm/SearchAnalysis';
import Select from './Select/Select';

const StreamingRepresentation = () => {
  const [selectedStreaming, setSelectedStreaming] = useState('');
  const [streamingType, setStreamingType] = useState('');
  const [data, setData] = useState('');
  const [search, setSearch] = useState('');
  useFetch('https://localhost:7282/Streamings', setData);
  const signatureOptions = [
    { name: 'Assinatura', id: 'subscription' },
    { name: 'AddOn', id: 'addon' },
    { name: 'Comprar', id: 'buy' },
    { name: 'Alugar', id: 'rent' },
  ];
  let url;
  if (search && !selectedStreaming && !streamingType) {
    url = `https://localhost:7282/Items?offset=60&name=${search}`;
  } else {
    url =
      selectedStreaming || streamingType || search
        ? `https://localhost:7282/Streamings/${selectedStreaming}${
            streamingType ? '?streamingType=' + streamingType + '&' : '?'
          }sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=60&${
            search ? 'name=' + search : ''
          }`
        : 'https://localhost:7282/Items?offset=60';
  }
  return (
    <div className={styles.background}>
      <SideMenu />
      <div className={`${styles.catalogo}`}>
        {data && (
          <div className={styles.selects}>
            <Select
              setItem={setStreamingType}
              jsonOptions={signatureOptions}
              texto={'Selecione a forma'}
            />
            <Select
              setItem={setSelectedStreaming}
              jsonOptions={data}
              texto={'Selecione um Streaming'}
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
