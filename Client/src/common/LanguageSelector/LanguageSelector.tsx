import React from "react";
import { Button, ButtonGroup } from "@mui/material";
import { useTranslation } from "react-i18next";

const LanguageSelector: React.FC = () => {
  const { i18n } = useTranslation();

  const changeLanguage = (lng: string) => {
    i18n.changeLanguage(lng);
  };

  return (
    <ButtonGroup variant="outlined" size="small">
      <Button onClick={() => changeLanguage("fi")}>FI</Button>
      <Button onClick={() => changeLanguage("en")}>EN</Button>
    </ButtonGroup>
  );
};

export default LanguageSelector;
