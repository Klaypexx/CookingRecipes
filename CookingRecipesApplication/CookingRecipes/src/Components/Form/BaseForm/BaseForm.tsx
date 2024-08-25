/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { Formik, Form } from 'formik';
import classNames from 'classnames';
import styles from './BaseFormLabel.module.css';
import FormProps from '../../../Types/FormProps';

const BaseForm: React.FC<FormProps> = ({ primary, id, initialValues, validationSchema, onSubmit, children }) => {
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
