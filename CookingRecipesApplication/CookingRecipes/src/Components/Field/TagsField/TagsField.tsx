import React, { useState } from 'react';
import styles from './TagsField.module.css';
import closeIcon from '../../../resources/icons/closeTag.svg';
import { ErrorMessage, FieldArray, FieldArrayRenderProps } from 'formik';
import TagsFieldProps from '../../../Types/TagsFieldProps';

const TagsField: React.FC<TagsFieldProps> = ({ name }) => {
  const [error, setError] = useState<string | null>(null); // Локальное состояние для ошибо

  const handleCreate = (
    e: React.KeyboardEvent<HTMLInputElement>,
    tags: string[],
    arrayHelpers: FieldArrayRenderProps,
  ) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      const inputValue = (e.target as HTMLInputElement).value.trim();

      if (inputValue.length > 20) {
        setError('Тег не должен превышать 20 символов');
        return;
      }

      if (tags.length >= 3) {
        setError('Вы не можете добавить больше 3 тегов'); // Установка ошибки
        return;
      }

      if (tags.some((tag: string) => tag.toLowerCase() === inputValue.toLowerCase())) {
        setError('Этот тег уже существует');
        return;
      }

      if (inputValue) {
        arrayHelpers.push(inputValue);
        (e.target as HTMLInputElement).value = '';
        setError(null);
      }
    }
  };

  const handleDelete = (arrayHelpers: FieldArrayRenderProps, index: number) => {
    arrayHelpers.remove(index);
    setError(null);
  };

  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        const tags: string[] = arrayHelpers.form.values[name] || [];

        return (
          <div className={styles.tagsFieldBox}>
            <div className={styles.tagsContainer}>
              {tags.length > 0
                ? tags.map((tag, index: number) => (
                    <div key={index} className={styles.tag}>
                      <span className={styles.name}>{tag}</span>
                      <img
                        src={closeIcon}
                        alt="closeIcon"
                        className={styles.icon}
                        onClick={() => handleDelete(arrayHelpers, index)}
                      />
                    </div>
                  ))
                : null}
              <input
                className={styles.tagInput}
                type="text"
                placeholder="Добавить теги"
                maxLength={20}
                onKeyDown={(event) => handleCreate(event, tags, arrayHelpers)}
              />
            </div>
            <ErrorMessage name={`${name}`} component="div" className={styles.baseErrorStyle} />
            {error && <div className={styles.baseErrorStyle}>{error}</div>}
          </div>
        );
      }}
    />
  );
};

export default TagsField;
