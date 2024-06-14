import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Component/Home/Home';
import Analise from './Component/MenuAnalise/Analise';
import StreamingRepresentation from './Component/StreamingRepresentation/StreamingRepresentation.jsx';
import StreamingComparison from './Component/StreamingComparison/StreamingComparison.jsx';
import SelectStreamings from './Component/StreamingComparison/SelectStreamings.jsx';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Analise" element={<Analise />} />
          <Route path="/Analysis" element={<StreamingRepresentation />} />
          <Route path="/Comparision" element={<SelectStreamings />} />
          <Route path="/Comparision/*" element={<StreamingComparison />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
