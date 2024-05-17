import { useEffect } from 'react';

function useFetch(url, setData, setLoading, setError) {
  useEffect(() => {
    if (setLoading && setData && setError && url) {
      setLoading('loading...');
      fetch(url)
        .then((res) => {
          if (res.ok) {
            return res.json();
          }
          throw new Error('Something went wrong');
        })
        .then((json) => {
          setData(json);
          setError(false);
        })
        .catch((er) => {
          setError(er);
        })
        .finally(() => {
          setLoading(false);
        });
    }
  }, [url]);

  return {};
}

export default useFetch;
