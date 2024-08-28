import { useState } from 'react';
import BaseField from '../BaseField/BaseField';
import TagsField from '../TagsField/TagsField';
import styles from './CardField.module.css';
import { ErrorMessage, useFormikContext } from 'formik';
import cloudDownload from '../../../resources/icons/cloud-download.svg';
import { COOKING_TIMES, DESCRIPTION_MAX_WORDS, PORTION_COUNT } from '../../../Constants/recipe';
import RecipeFormValues from '../../../Types/RecipeFormValues';
import { IMAGE_URL } from '../../../Constants/httpUrl';

const CardField = () => {
  const { setFieldValue, initialValues } = useFormikContext<RecipeFormValues>();
  const [file, setFile] = useState<File | null>(null);
  const [avatarPath, setAvatarPath] = useState(initialValues.avatarPath);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.currentTarget.files) {
      const selectedFile = event.currentTarget.files[0];
      setAvatarPath(undefined);
      setFile(selectedFile);
      setFieldValue('avatar', selectedFile);
    }
  };

  return (
    <div className={styles.cardContainer}>
      <div className={styles.cardPhotoBox}>
        {(file || avatarPath) && (
          <img
            src={file ? URL.createObjectURL(file) : IMAGE_URL + avatarPath}
            alt="avatar"
            className={styles.avatarImage}
          />
        )}
        <input
          type="file"
          name="avatar"
          accept="image/png, image/jpeg"
          onChange={handleFileChange}
          className={styles.inputAvatar}
        />
        <div className={styles.inputPhoto}>
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
        <div className={styles.cardFormBox}>
          <BaseField
            className={styles.inputRecipeNameFormSize}
            margin
            name="name"
            type="text"
            labelText="Название рецепта"
          />
          <BaseField
            margin
            className={styles.textareaFormSize}
            as="textarea"
            name="description"
            placeholder={`Краткое описание рецепта (${DESCRIPTION_MAX_WORDS} символов)`}
          />
          <TagsField name="tags" />
          <div className={styles.optionBox}>
            <div className={styles.optionFlex}>
              <BaseField select as="select" name="cookingTime" className={styles.optionForm}>
                <option value="">Время готовки</option>
                {COOKING_TIMES.map((time, index) => (
                  <option key={index} value={time}>
                    {time}
                  </option>
                ))}
              </BaseField>
              <p className={styles.optionText}>Минут</p>
            </div>
            <div className={styles.optionFlex}>
              <BaseField select as="select" name="portion" className={styles.optionForm}>
                <option value="">Порций в блюде</option>
                {PORTION_COUNT.map((portion, index) => (
                  <option key={index} value={portion}>
                    {portion}
                  </option>
                ))}
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
