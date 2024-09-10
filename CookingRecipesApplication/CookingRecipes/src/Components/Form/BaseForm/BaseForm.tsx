import classNames from 'classnames';
import { Form, Formik } from 'formik';
import React from 'react';
import BaseFormProps from '../../../Types/BaseFormProps';
import styles from './BaseFormLabel.module.css';

const BaseForm: React.FC<BaseFormProps> = ({ primary, id, initialValues, validationSchema, onSubmit, children }) => {
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={onSubmit}
      enableReinitialize={true}
    >
      {() => (
        <Form id={id} className={classNames(primary && styles.baseFormStyle)}>
          {children}
        </Form>
      )}
    </Formik>
  );
};

export default BaseForm;
