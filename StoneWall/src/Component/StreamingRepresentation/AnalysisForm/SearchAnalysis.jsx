import { useState, useEffect } from 'react';
import styles from './AnalysisForm.module.css';

const SearchAnalysis = ({ setSearch }) => {
  const [query, setQuery] = useState('');

  useEffect(() => {
    const timeOutId = setTimeout(() => setSearch(query), 500);
    return () => clearTimeout(timeOutId);
  }, [query, setSearch]);
  function handleSubmit(e) {
    e.preventDefault();
    const form = e.target;
    const formData = new FormData(form);
    const formJson = Object.fromEntries(formData.entries());
    setSearch(formJson.search);
  }
  return (
    <form method="post" onSubmit={handleSubmit}>
      <input
        type="search"
        name="search"
        id="search"
        className={styles.input}
        placeholder="Pesquisar"
        onChange={(e) => setQuery(e.target.value)}
      />
    </form>
  );
};

export default SearchAnalysis;
