import styles from './Error.module.css';

const Error = ({ error }) => {
  return <div className={styles.error}>{error.message}</div>;
};

export default Error;
