import { useState } from 'react';
import styles from './StreamingComparison.module.css';
import useFetch from '../useFetch';
import SideMenu from '../SideMenu/SideMenu';
import Select from '../StreamingRepresentation/Select/Select';
import SearchAnalysis from '../StreamingRepresentation/AnalysisForm/SearchAnalysis';
import ItemsCatalogo from '../StreamingRepresentation/ItemsCatalogo/ItemsCatalogo';

const StreamingRepresentation = ({ streaming1, streaming2 }) => {
  const [selectedStreaming1, setSelectedStreaming1] = useState(streaming1);
  const [selectedStreaming2, setSelectedStreaming2] = useState(streaming2);
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
  let url = `https://localhost:7282/compare/${selectedStreaming1}-${selectedStreaming2}?offset=60${
    search ? '&name=' + search : ''
  }${streamingType ? '&streamingType=' + streamingType : ''}`;
  return (
    <div className={styles.background}>
      <SideMenu />
      <div className={`${styles.catalogo}`}>
        {data && (
          <div className={styles.selects}>
            <Select
              setItem={setStreamingType}
              jsonOptions={signatureOptions}
              texto={'Selecione o tipo de Streaming'}
            />
            <Select
              setItem={setSelectedStreaming2}
              jsonOptions={data}
              texto={'Selecione o segundo Streaming'}
            />
            -
            <Select
              setItem={setSelectedStreaming1}
              jsonOptions={data}
              texto={'Selecione o primeiro Streaming'}
            />
            <SearchAnalysis setSearch={setSearch} />
          </div>
        )}
        {selectedStreaming1 && selectedStreaming2 && (
          <ItemsCatalogo urlFetch={url} />
        )}
      </div>
    </div>
  );
};

export default StreamingRepresentation;
