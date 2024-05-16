import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Component/Home/Home';
import Analise from './Component/MenuAnalise/Analise';
import StreamingRepresentation from './Component/StreamingRepresentation/StreamingRepresentation.jsx';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Analise" element={<Analise />} />
          <Route path="/Streaming" element={<StreamingRepresentation />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
