/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { Formik, Form } from 'formik';
import classNames from 'classnames';
import styles from './BaseFormLabel.module.css';
import BaseFormProps from '../../../Types/BaseFormProps';

const BaseForm: React.FC<BaseFormProps> = ({ primary, id, initialValues, validationSchema, onSubmit, children }) => {
  return (
    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
      {() => (
        <Form id={id} className={classNames(primary && styles.baseFormStyle)}>
          {children}
        </Form>
      )}
    </Formik>
  );
};

export default BaseForm;
