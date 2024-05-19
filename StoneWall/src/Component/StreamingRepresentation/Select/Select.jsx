import styles from './Select.module.css';
import PropTypes from 'prop-types';

const Select = ({ setItem, jsonOptions }) => {
  return (
    jsonOptions &&
    setItem && (
      <select
        onChange={(e) => setItem(e.target.value)}
        className={styles.select}
      >
        <option value="">
          Selecione uma opção:{' '}
        </option>
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
  jsonOptions: PropTypes.array,
};

export default Select;
