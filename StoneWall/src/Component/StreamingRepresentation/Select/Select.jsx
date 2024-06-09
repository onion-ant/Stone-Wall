import styles from './Select.module.css';
import PropTypes from 'prop-types';

const Select = ({ setItem, jsonOptions, texto, streamingIsSelected }) => {
  return (
    jsonOptions &&
    setItem && (
      <select
        onChange={(e) => {
          setItem(e.target.value);
          if (streamingIsSelected) {
            streamingIsSelected(e.target.value);
          }
          e.target.value;
        }}
        className={styles.select}
      >
        <option value="">{texto} </option>
        {jsonOptions.map((option, index) => (
          <option key={index} value={option.id}>
            {option.name}
          </option>
        ))}
      </select>
    )
  );
};

Select.propTypes = {
  setItem: PropTypes.func,
  jsonOptions: PropTypes.array,
  texto: PropTypes.string,
  streamingIsSelected: PropTypes.func,
};

export default Select;
