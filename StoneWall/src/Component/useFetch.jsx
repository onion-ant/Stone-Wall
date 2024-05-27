import { useEffect } from 'react';

function useFetch(url, setData, setLoading, setError, data) {
  useEffect(() => {
    setLoading ? setLoading(true) : '';
    fetch(url)
      .then((res) => {
        if (res.ok) {
          return res.json();
        }
        throw new Error('Something went wrong');
      })
      .then((json) => {
        data ? setData([...data, ...json]) : setData(json)
        setError(false);
      })
      .catch((er) => {
        setError ? setError(er) : '';
      })
      .finally(() => {
        setLoading ? setLoading(false) : '';
      });
  }, [url, setData, setLoading, setError]);

  return {};
}

export default useFetch;
