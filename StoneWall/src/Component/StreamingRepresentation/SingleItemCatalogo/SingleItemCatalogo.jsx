import { useState, useRef } from 'react';
import styles from './SingleItemCatalogo.module.css';
import ModalCatalogo from '../ModalCatalogo/ModalCatalogo';
import PropTypes from 'prop-types';

const SingleItemCatalogo = ({ posterPath, tmdbId }) => {
  const [isOpen, setIsOpen] = useState(false);
  const modalRef = useRef();
  function handleClick() {
    setIsOpen(!isOpen);
  }
  function closeModal() {
    setIsOpen(false);
  }
  function handleOutsideModalClick(event) {
    event.stopPropagation();
    if (modalRef.current) {
      if (
        !modalRef.current.firstChild.contains(event.target) &&
        isOpen == true
      ) {
        setIsOpen(false);
      }
    }
  }
  return (
    <>
      <img
        className={styles.itemCatalogo}
        src={posterPath}
        alt=""
        onClick={() => handleClick()}
      />
      {isOpen && (
        <div
          ref={modalRef}
          className={styles.modal}
          onClick={handleOutsideModalClick}
        >
          <ModalCatalogo tmdbId={tmdbId} onClose={closeModal} />
        </div>
      )}
    </>
  );
};

SingleItemCatalogo.propTypes = {
  posterPath: PropTypes.string,
  tmdbId: PropTypes.number,
};

export default SingleItemCatalogo;
