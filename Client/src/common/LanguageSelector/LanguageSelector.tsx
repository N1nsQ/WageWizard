import { Button, ButtonGroup } from "@mui/material";
import { useTranslation } from "react-i18next";

const LanguageSelector = () => {
  const { i18n } = useTranslation();

  const changeLanguage = (lng: string) => {
    i18n.changeLanguage(lng);
  };

  return (
    <div className="language-selector">
      <ButtonGroup variant="outlined" size="small">
        <Button onClick={() => changeLanguage("fi")}>FI</Button>
        <Button onClick={() => changeLanguage("en")}>EN</Button>
      </ButtonGroup>
    </div>
  );
};

export default LanguageSelector;
