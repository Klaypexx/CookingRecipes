import { ErrorMessage, FieldArray, FieldArrayRenderProps } from 'formik';
import React, { useState } from 'react';
import { TAG_MAX_WORDS, TAGS_MAX_COUNT } from '../../../Constants/recipe';
import closeIcon from '../../../resources/icons/closeTag.svg';
import TagsFieldProps from '../../../Types/TagsFieldProps';
import styles from './TagsField.module.css';

const TagsField: React.FC<TagsFieldProps> = ({ name }) => {
  const [error, setError] = useState<string | null>(null);
  const [inputValue, setInputValue] = useState<string>('');

  const handleCreate = (value: string, tags: Array<{ name: string }>, arrayHelpers: FieldArrayRenderProps) => {
    setError(null);
    const trimmedValue = value.trim();

    if (trimmedValue.length > TAG_MAX_WORDS) {
      setError('Тег не должен превышать 20 символов');
      return;
    }

    if (tags.length >= TAGS_MAX_COUNT) {
      setError('Вы не можете добавить больше 3 тегов');
      return;
    }

    if (tags.some((tag) => tag.name.toLowerCase() === trimmedValue.toLowerCase())) {
      setError('Этот тег уже существует');
      return;
    }

    if (trimmedValue) {
      arrayHelpers.push({ name: trimmedValue });
      setInputValue('');
      setError(null);
    }
  };

  const handleKeyDown = (
    event: React.KeyboardEvent<HTMLInputElement>,
    tags: Array<{ name: string }>,
    arrayHelpers: FieldArrayRenderProps,
  ) => {
    if (event.key === 'Enter') {
      event.preventDefault();
      handleCreate(inputValue, tags, arrayHelpers);
    }
  };

  const handleBlur = (tags: Array<{ name: string }>, arrayHelpers: FieldArrayRenderProps) => {
    handleCreate(inputValue, tags, arrayHelpers);
  };

  const handleDelete = (arrayHelpers: FieldArrayRenderProps, index: number) => {
    arrayHelpers.remove(index);
    setError(null);
  };

  return (
    <FieldArray
      name={name}
      render={(arrayHelpers: FieldArrayRenderProps) => {
        const tags: Array<{ name: string }> = arrayHelpers.form.values[name] || [];
        return (
          <div className={styles.tagsFieldBox}>
            <div className={styles.tagsContainer}>
              {tags.length > 0
                ? tags.map((tag, index: number) => (
                    <div key={index} className={styles.tag}>
                      <p className={styles.name}>{tag.name}</p>
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
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
                onKeyDown={(event) => handleKeyDown(event, tags, arrayHelpers)}
                onBlur={() => handleBlur(tags, arrayHelpers)}
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
