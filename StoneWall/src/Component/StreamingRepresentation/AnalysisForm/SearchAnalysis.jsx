import { useState, useEffect } from 'react';

const SearchAnalysis = ({ setSearch }) => {
  const [query, setQuery] = useState('');

  useEffect(() => {
    const timeOutId = setTimeout(() => setSearch(query), 500);
    return () => clearTimeout(timeOutId);
  }, [query]);
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
        className="search-input"
        placeholder="Search user"
        onChange={(e) => setQuery(e.target.value)}
      />
    </form>
  );
};

export default SearchAnalysis;
