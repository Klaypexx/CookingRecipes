import React, { useState } from 'react';
import styles from './TagsInput.module.css';
import closeIcon from '../../../resources/icons/closeTag.svg';
import { FieldArray, FieldArrayRenderProps } from 'formik';
import { TagsInputProps } from '../../../Types/types';

const TagsInput: React.FC<TagsInputProps> = ({ name }) => {
  const [error, setError] = useState<string | null>(null); // Локальное состояние для ошибок

  const handleCreate = (e: React.KeyboardEvent<HTMLInputElement>, tags: any, arrayHelpers: FieldArrayRenderProps) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      const inputValue = (e.target as HTMLInputElement).value.trim();

      if (tags.length >= 5) {
        setError('Вы не можете добавить больше 5 тегов'); // Установка ошибки
        return;
      }

      if (tags.some((tag: any) => tag.toLowerCase() === inputValue.toLowerCase())) {
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
    <>
      <FieldArray
        name={name}
        render={(arrayHelpers: FieldArrayRenderProps) => {
          const tags = arrayHelpers.form.values[name] || [];

          return (
            <div className={styles.tagsInputBox}>
              <div className={styles.tagsContainer}>
                {tags.length > 0
                  ? tags.map((tag: string, index: number) => (
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
                  maxLength={15}
                  onKeyDown={(event) => handleCreate(event, tags, arrayHelpers)}
                />
              </div>
              {error && <div className={styles.baseErrorStyle}>{error}</div>}
            </div>
          );
        }}
      />
    </>
  );
};

export default TagsInput;
