import { useEffect } from 'react';

function useFetch(url, setData, setLoading, setError, setNextCursor) {
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
        json.items ? setData(json.items) : setData(json)
        setNextCursor && setNextCursor(json.nextCursor)
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
