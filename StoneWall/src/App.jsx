import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Component/Home/Home';
import Analise from './Component/MenuAnalise/Analise';
import StreamingAnalysis from './Component/StreamingAnalysis/StreamingAnalysis';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Analise" element={<Analise />} />
          <Route path="/Streaming" element={<StreamingAnalysis />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
