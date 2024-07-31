import { useState } from 'react';
import BaseField from '../BaseField/BaseField';
import TagsField from '../TagsField/TagsField';
import styles from './CardField.module.css';
import { ErrorMessage, useFormikContext } from 'formik';
import cloudDownload from '../../../resources/icons/cloud-download.svg';

const CardField = () => {
  const { setFieldValue } = useFormikContext();
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.currentTarget.files) {
      const selectedFile = event.currentTarget.files[0];
      setFile(selectedFile);
      setFieldValue('avatar', selectedFile); // Store the file directly
    }
  };
  return (
    <div className={styles.cardContainer}>
      <div className={styles.cardBackground}></div>
      <div className={styles.cardPhoto}>
        {file && <img src={URL.createObjectURL(file)} alt="avatar" className={styles.avatarImage} />}
        <input
          type="file"
          name="avatar"
          accept="image/png, image/jpeg"
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
      </div>
      <div className={styles.cardInformation}>
        <div className={styles.cardFormContainer}>
          <BaseField
            className={styles.inputRecipeNameFormSize}
            margin
            name="name"
            type="text"
            placeholder="Название рецепта"
          />
          <BaseField
            margin
            className={styles.textareaFormSize}
            as="textarea"
            name="description"
            placeholder="Краткое описание рецепта (150 символов)"
            maxLength={150}
          />
          <TagsField name="tags" />
          <div className={styles.optionContainer}>
            <div className={styles.optionBox}>
              <BaseField select as="select" name="cookingTime" className={styles.optionForm}>
                <option value="">Время готовки</option>
                <option value="5">5</option>
                <option value="10">10</option>
                <option value="15">15</option>
                <option value="30">30</option>
                <option value="35">35</option>
                <option value="40">40</option>
                <option value="60">60</option>
              </BaseField>
              <p className={styles.optionText}>Минут</p>
            </div>
            <div className={styles.optionBox}>
              <BaseField select as="select" name="portion" className={styles.optionForm}>
                <option value="">Порций в блюде</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
              </BaseField>
              <p className={styles.optionText}>Персон</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CardField;
