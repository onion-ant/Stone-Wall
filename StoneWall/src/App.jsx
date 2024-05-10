import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Component/Home/Home';
import Analise from './Component/Analise/Analise';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Analise" element={<Analise />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
