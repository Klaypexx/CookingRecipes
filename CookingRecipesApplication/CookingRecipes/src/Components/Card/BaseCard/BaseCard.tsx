import { ErrorMessage, useFormikContext } from 'formik';
import classNames from 'classnames';
import styles from './BaseCard.module.css';
import { CardProps } from '../../../Types/types';
import cloudDownload from '../../../resources/icons/cloud-download.svg';
import { useState } from 'react';

const BaseCard: React.FC<CardProps> = ({ form, margin, className, children }) => {
  const [file, setFile] = useState('');
  const { setFieldValue } = useFormikContext();

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.currentTarget.files) {
      const selectedFile = event.currentTarget.files[0];
      const fileURL = URL.createObjectURL(selectedFile).toString();
      setFile(fileURL);
      setFieldValue('avatar', fileURL);
    }
  };

  return (
    <div className={classNames(margin ? styles.margin : undefined, styles.cardContainer, className)}>
      <div className={styles.cardBackground}></div>
      <div className={styles.cardPhoto}>
        {file && <img src={file} alt="avatar" className={styles.avatarImage} />}
        {form && (
          <>
            <input
              type="file"
              name="avatar"
              accept="image/*"
              onChange={handleFileChange}
              className={styles.inputPhoto}
            />
            <div className={styles.inputPhotoContainer}>
              {!file && (
                <div className={styles.inputPhotoBox}>
                  <img src={cloudDownload} alt="cloud download" className={styles.cloudDownload} />
                  <p className={styles.inputPhotoText}>Загрузите фото готового блюда</p>
                </div>
              )}
            </div>
            <ErrorMessage name="avatar" component="div" className={styles.baseErrorStyle} />
          </>
        )}
      </div>
      <div className={styles.cardInformation}>{children}</div>
    </div>
  );
};

export default BaseCard;
